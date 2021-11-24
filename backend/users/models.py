from django.db import models
from django.contrib.auth.models import AbstractUser

# Create your models here.


class User(AbstractUser):
    is_student = models.BooleanField(default=False)
    is_teacher = models.BooleanField(default=False)

    def __str__(self) -> str:
        return self.username


class Student(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE, related_name="user")
    average_grade = models.IntegerField(default=5, null=True, blank=True)

    def __str__(self) -> str:
        return self.user.username
    

class Teacher(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE)

    def __str__(self) -> str:
        return self.user.username


class Subject(models.Model):
    student = models.ManyToManyField(Student, related_name="student")
    title = models.CharField(max_length=200, null=False)
    description = models.CharField(max_length=200, null=True, blank=True)
    # test = tu ne ide nista ali postoji

    objects = models.Manager()

    def __str__(self):
        return self.title
