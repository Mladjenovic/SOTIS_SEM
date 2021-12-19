from django.shortcuts import render
from rest_framework.response import Response
from rest_framework.decorators import api_view, permission_classes

import pandas as pd
import numpy as np

import sys
sys.path.insert(0,'C:\\Users\\cvijetinm\\Desktop\\sotis_2\\SOTIS_SEM\\KST\\microservice_kst\\iita\\learning_spaces')
# sys.path.append('learning_spaces/')

from kst import iita 


# from .learning_spaces.kst import iita
# from .learning_spaces.kst import iita 
# Create your views here.

@api_view(['POST'])
def iita_view(request):
    data_frame = pd.DataFrame({'a': [1, 0, 1], 'b': [0, 1, 0], 'c': [0, 1, 1]})
    response = iita(data_frame, v=1)

    return Response(response)
