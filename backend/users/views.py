import json
from django.shortcuts import render
from rest_framework.decorators import api_view, permission_classes
from rest_framework.permissions import IsAuthenticated, IsAdminUser
from rest_framework.response import Response

from .models import User, Student, Teacher
from .serializers import  UserSerializer, CustomRegisterSerializer, SubjectSerializer

from django.contrib.auth.hashers import make_password
from django.contrib.auth import authenticate
from rest_framework import status

from rest_framework import viewsets
from django.shortcuts import render

from .models import User, Student
from .serializers import UserSerializer, StudentSerializer



@api_view(['POST'])
def registerUser(request):
    data = request.data
    try:
        serializer = StudentSerializer(data=data)
        if serializer.is_valid():
            try:
                serializer.save()
                
                # crypt password
                createdUser = User.objects.get(username=data['user']['username'])
                createdUser.password = make_password(createdUser.password)
                createdUser.save()
            except Exception as e:
                print(str(e))
        else:
            message = {'detail': serializer.error}
            return Response(message, status=status.HTTP_400_BAD_REQUEST)
           
        return Response(serializer.data)
    except Exception as e:
        message = {'detail': serializer.errors}
        return Response(message, status=status.HTTP_400_BAD_REQUEST)
    
@api_view(['POST'])
def loginUser(request):
    data = request.data
    print(data)
    try:
        check_if_user_exists = User.objects.filter(username=data.get('username')).exists()
        print(check_if_user_exists)
        if check_if_user_exists:
            try:
                user = authenticate(request, username=data.get('username'), password=(data.get('password')))
            except Exception as e:
                print(str(e))
            if user is not None:
                # this user is valid, do what you want to do
                serializer = UserSerializer(user, many=False)
                return Response(serializer.data)
            else:
                # this user is not valid, he provided wrong password, show some error message
                message = {'detail': 'Wrong password'}
                return Response(message, status=status.HTTP_400_BAD_REQUEST)
            
        else:
            message = {'detail': 'There is no such user'}
            return Response(message, status=status.HTTP_204_NO_CONTENT)
    except:
        message = {'detail': 'Couldn\'t login user'}
        return Response(message, status=status.HTTP_400_BAD_REQUEST)
    
    
@api_view(['GET'])
def getUsers(request):
    if request.method == 'GET':
        users = User.objects.all()
        serializer = UserSerializer(users, many=True)
        return Response(serializer.data)



@api_view(['POST'])
def subjectView(request):
    data = request.data
    try:
        serializer = SubjectSerializer(data=data)
        if serializer.is_valid():
            try:
                serializer.save()
            except Exception as e:
                print(str(e))
        else:
            message = {'detail': serializer.error}
            return Response(message, status=status.HTTP_400_BAD_REQUEST)
           
        return Response(serializer.data)
    except Exception as e:
        message = {'detail': serializer.errors}
        return Response(message, status=status.HTTP_400_BAD_REQUEST)



