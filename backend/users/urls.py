from django.urls import path, include, re_path
from users import views

urlpatterns =[
    path('register/', views.registerUser, name='register'),
    path('login/', views.loginUser, name='login'),
    path('get-users/', views.getUsers, name='get-users'),
    path('create-subject/', views.subjectView, name='subject-view'),
]
