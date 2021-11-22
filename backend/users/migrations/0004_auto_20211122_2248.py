# Generated by Django 3.2.8 on 2021-11-22 21:48

from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('users', '0003_student_average_grade'),
    ]

    operations = [
        migrations.AlterField(
            model_name='student',
            name='average_grade',
            field=models.IntegerField(blank=True, default=5, null=True),
        ),
        migrations.AlterField(
            model_name='student',
            name='user',
            field=models.OneToOneField(on_delete=django.db.models.deletion.CASCADE, related_name='user', to=settings.AUTH_USER_MODEL),
        ),
        migrations.CreateModel(
            name='Subject',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('title', models.CharField(max_length=200)),
                ('description', models.CharField(blank=True, max_length=200, null=True)),
                ('student', models.ManyToManyField(related_name='student', to='users.Student')),
            ],
        ),
    ]
