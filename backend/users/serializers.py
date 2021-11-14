from allauth.account.adapter import get_adapter
from rest_auth.registration.serializers import RegisterSerializer
from rest_framework import serializers
from rest_framework.authtoken.models import Token

from .models import User, Student


class UserSerializer(serializers.ModelSerializer):
    class Meta: 
        model = User
        fields = ('email', 'username', 'password', 'is_student', 'is_teacher')


class CustomRegisterSerializer(RegisterSerializer):
    is_student = serializers.BooleanField()
    is_teacher = serializers.BooleanField()
    first_name = serializers.CharField()
    last_name = serializers.CharField()
    
    class Meta: 
        model = User
        fields = ('first_name', 'last_name', 'email', 'username', 'password', 'is_student', 'is_teacher')

    def get_cleaned_data(self):
        return {
            'username' : self.validated_data.get('username', ''),
            'first_name' : self.validated_data.get('first_name'),
            'last_name' : self.validated_data.get('last_name'),
            'password1' : self.validated_data.get('password1', ''),
            'password2' : self.validated_data.get('password2', ''),
            'email' : self.validated_data.get('email'),
            'is_student' : self.validated_data.get('is_student'),
            'is_teacher' : self.validated_data.get('is_teacher')
        }

    def save(self, request):
        user = UserSerializer(request)
        user.is_student = self.cleaned_data.get('is_student')
        user.is_teacher = self.cleaned_data.get('is_teacher')
        user.save()

        return user

class TokenSerializer(serializers.ModelSerializer):
    user_type = serializers.SerializerMethodField()

    class Meta:
        model = Token
        fields = ('key', 'user', 'user_type')

    def get_user_type(self, obj):
        serializer_data = UserSerializer(
            obj.user
        ).data
        is_student = serializer_data.get('is_student')
        is_teacher = serializer_data.get('is_teacher')
        return {
            'is_student': is_student,
            'is_teacher': is_teacher
        }
    
class StudentSerializer(serializers.ModelSerializer):
    class Meta: 
        model = Student
        fields = "__all__"