from allauth.account.adapter import get_adapter
from rest_auth.registration.serializers import RegisterSerializer
from rest_framework import serializers
from rest_framework.authtoken.models import Token
from django.contrib.auth.hashers import make_password
from drf_writable_nested import WritableNestedModelSerializer


from .models import User, Student, Subject


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
    
class StudentSerializer(WritableNestedModelSerializer, serializers.ModelSerializer):
    user = UserSerializer()
    
    class Meta: 
        model = Student
        fields = ["user", "average_grade"]
        
        def save(self):
            user = self.validated_data['user']
            user.password = make_password(user.password)
            average_grade = self.validated_data['average_grade']
        
        def create(self, validated_data):
            user_data = validated_data.pop('user')
            usre_data.password = make_password(user_data.password)
            student = Student.objects.create(**validated_data)
            User.objects.create(student=student, **user_data)
            return student
        
class SubjectSerializer(WritableNestedModelSerializer, serializers.ModelSerializer):
    student = StudentSerializer(many=True)
    
    class Meta: 
        model = Subject
        fields = ["student", "title", "description"]
    