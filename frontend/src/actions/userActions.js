import axios from "axios";

import * as actions from "../constants/userConstants";

export const login = (username, password) => async (dispatch) => {
  try {
    dispatch({
      type: actions.USER_LOGIN_REQUEST,
    });

    const config = {
      headers: {
        "Content-type": "application/json",
      },
    };

    const { data } = await axios.post(
      "/rest-auth/login/",
      { username: username, password: password },
      config
    );

    dispatch({
      type: actions.USER_LOGIN_SUCCESS,
      payload: data,
    });

    localStorage.setItem("userInfo", JSON.stringify(data));
  } catch (error) {
    dispatch({
      type: actions.USER_LOGIN_FAIL,
      payload:
        error.response && error.response.data.detail
          ? error.response.data.detail
          : error.message,
    });
  }
};

export const logout = () => (dispatch) => {
  localStorage.removeItem("userInfo");
  dispatch({ type: actions.USER_LOGOUT });
};

export const register =
  (firstname, lastname, username, email, password, userType) =>
  async (dispatch) => {
    try {
      dispatch({
        type: actions.USER_REGISTER_REQUEST,
      });

      const config = {
        headers: {
          "Content-type": "application/json",
        },
      };

      const user = {
        firstname: firstname,
        lastname: lastname,
        username: username,
        email: email,
        password: password,
        userType: userType,
      };

      const { data } = await axios.post(
        "http://localhost:8000/api/users/register/",
        user,
        config
      );

      dispatch({
        type: actions.USER_REGISTER_SUCCESS,
        payload: data,
      });

      dispatch({
        type: actions.USER_LOGIN_SUCCESS,
        payload: data,
      });

      localStorage.setItem("userInfo", JSON.stringify(data));
    } catch (error) {
      dispatch({
        type: actions.USER_REGISTER_FAIL,
        payload:
          error.response && error.response.data.detail
            ? error.response.data.detail
            : error.message,
      });
    }
  };
