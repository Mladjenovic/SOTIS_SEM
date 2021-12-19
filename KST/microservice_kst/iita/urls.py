from django.urls import path, include, re_path
from iita import views 

urlpatterns =[
    path('', views.iita_view, name='iita-view'),
]
