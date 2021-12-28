import sys
import json
import pandas as pd
from flask_cors import CORS
from flask import Flask, request, jsonify, make_response

from learning_spaces.kst import iita

sys.path.append('learning_spaces/')

app = Flask(__name__)
CORS(app)

@app.route("/", methods=['POST','GET'])
def default_example():
   if request.method == 'GET':
        data_frame = pd.DataFrame({'a': [1, 0, 1], 'b': [0, 1, 0], 'c': [0, 1, 1]})
        print("\n--------------------------------\n", data_frame, "\n--------------------------------\n")
        response = iita(data_frame, v=1)
        print("\n--------------------------------\n", response, "\n--------------------------------\n")
        return pd.Series(response).to_json(orient='values')
      

@app.route('/products', methods=['GET'])
def products():
  if request.method == 'GET':
    products = [{"id": "1", "name": "name"}, {"id": "2", "name": "name2"}]
    return json.dumps(products)



if __name__ == "__main__":
  app.run()