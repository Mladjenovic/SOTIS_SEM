import React from "react";
import { Route } from "react-router-dom";
import HomeScreen from "./screens/HomeScreen";

function BaseRouter() {
  return (
    <div>
      <Route exact path="/home" component={HomeScreen}></Route>
    </div>
  );
}

export default BaseRouter;
