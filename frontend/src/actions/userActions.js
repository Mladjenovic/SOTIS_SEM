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
      "https://localhost:44393/api/UsersContoller/Login",
      { UserName: username, Password: password },
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
        error.response && error.response.data
          ? error.response.data
          : error.message,
    });
  }
};

export const logout = () => (dispatch) => {
  localStorage.removeItem("userInfo");
  dispatch({ type: actions.USER_LOGOUT });
};

export const register =
  (username, password, email, fullname, userType) => async (dispatch) => {
    try {
      dispatch({
        type: actions.USER_REGISTER_REQUEST,
      });

      const config = {
        headers: {
          "Content-type": "application/json",
        },
      };

      const { data } = await axios.post(
        "https://localhost:44393/api/UsersContoller/Register",
        {
          Username: username,
          Password: password,
          Email: email,
          Fullname: fullname,
          UserType: userType,
        },
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
