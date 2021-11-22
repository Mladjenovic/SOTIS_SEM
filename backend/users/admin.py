from django.contrib import admin
from .models import User, Student, Teacher, Subject
from django.contrib.auth.models import Group
# Register your models here.

admin.site.register(User)
admin.site.register(Student)
admin.site.register(Teacher)
admin.site.register(Subject)
admin.site.unregister(Group)