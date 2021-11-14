from django.shortcuts import render
from rest_framework.decorators import api_view, permission_classes
from rest_framework.permissions import IsAuthenticated, IsAdminUser
from rest_framework.response import Response

from .models import User, Student
from .serializers import  UserSerializer, CustomRegisterSerializer

from django.contrib.auth.hashers import make_password
from rest_framework import status

from rest_framework import viewsets
from django.shortcuts import render

from .models import User
from .serializers import UserSerializer


class UserViewSet(viewsets.ModelViewSet):
    serializer_class = UserSerializer
    queryset = User.objects.all()


@api_view(['POST'])
def registerUser(request):
    data = request.data
    try:
        user = User.objects.create(
            first_name=data['firstname'],
            last_name=data['lastname'],
            username=data['username'],
            email=data['email'],
            password=make_password(data['password']), 
            is_student=True if (data['userType'] == 'student') else False,
            is_teacher=True if (data['userType'] == 'teacher') else False
        )
        
        if(user.is_student): 
            student = Student.objects.create(user=user, avgGrade=data['avgGrade'])
        

        serializer = CustomRegisterSerializer(user, many=False)
        
        
        
        return Response(serializer.data)
    except:
        message = {'detail': 'Couldn\'t register user'}
        return Response(message, status=status.HTTP_400_BAD_REQUEST)


    

