from django.contrib import admin
from .models import User, Student
from django.contrib.auth.models import Group
# Register your models here.

admin.site.register(User)
admin.site.register(Student)
admin.site.unregister(Group)