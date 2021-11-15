from django.contrib import admin
from .models import User, Student, Teacher
from django.contrib.auth.models import Group
# Register your models here.

admin.site.register(User)
admin.site.register(Student)
admin.site.register(Teacher)
admin.site.unregister(Group)