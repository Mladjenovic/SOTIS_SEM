# Generated by Django 3.2.8 on 2021-11-17 07:42

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('users', '0002_teacher'),
    ]

    operations = [
        migrations.AddField(
            model_name='student',
            name='average_grade',
            field=models.IntegerField(default=5),
        ),
    ]