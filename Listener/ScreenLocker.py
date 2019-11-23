import json
import os
from flask import Flask, jsonify, request

app = Flask(__name__)


@app.route("/")
def ReadInstructionsFile():
    data = {}
    with open("C:\\Users\\Ringba\\py\\instructions.json", 'r') as f:
        data = json.load(f)
    return jsonify(data)


# A route to return all of the available entries in our catalog.
@app.route('/', methods=['POST'])
def api_all():
    request_data = request.get_json()
    if validRequest(request_data):

        # read existing json to memory. you do this to preserve whatever existing data.
        with open("C:\\Users\\Ringba\\py\\instructions.json", 'r') as jsonfile:
            json_content = json.load(jsonfile)  # this is now in memory! you can use it outside 'open'

        # add the id key-value pair (rmbr that it already has the "name" key value)
        json_content["target"] = request_data["target"]
        json_content["message"] = request_data["message"]
        json_content["status"] = True
        json_content["IsCloseAllowed"] = False

        with open("C:\\Users\\Ringba\\py\\instructions.json", 'w') as jsonfile:
            json.dump(json_content, jsonfile, indent=4)  # you decide the indentation level

        os.startfile("C:\\Users\\Ringba\\py\\ScreenLocker.lnk")
        return "True"
    else:
        return "False"


@app.route('/', methods=['DELETE'])
def api_kill():
    # read existing json to memory. you do this to preserve whatever existing data.
    with open("C:\\Users\\Ringba\\py\\instructions.json", 'r') as jsonfile:
        json_content = json.load(jsonfile)  # this is now in memory! you can use it outside 'open'

        json_content["status"] = False
        json_content["IsCloseAllowed"] = True

    with open("C:\\Users\\Ringba\\py\\instructions.json", 'w') as jsonfile:
        json.dump(json_content, jsonfile, indent=4)  # you decide the indentation level

    os.system("taskkill /f /im  ScreenLocker.exe")
    return "True"


def validRequest(reqObject):
    if "target" in reqObject and "message" in reqObject:
        return True
    else:
        return False


if __name__ == "__main__":
    app.run()
