from django.urls import path, include, re_path

from users import views

urlpatterns =[
    path('register/', views.registerUser, name='register'),
]
