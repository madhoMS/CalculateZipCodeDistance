/* eslint-disable react-hooks/exhaustive-deps */
import React, { useState, useEffect } from "react";
import { stringValidate } from "./Validations";
import "./styles/style.css";

function FindDistance() {
  const [fromZipcode, setfromZipcode] = useState("");
  const [toZipcode, settoZipcode] = useState("");
  const [answer, setAnswer] = useState("");
  const [messageClass, setMessageClass] = useState("");
  const [formError, setFormError] = useState({
    fromZipcode: "",
    toZipcode: "",
  });

  const submitForm = (e) => {
    e.preventDefault();
    setFormError({
      ...formError,
      fromZipcode: stringValidate(fromZipcode, "From Zipcode"),
      toZipcode: stringValidate(toZipcode, "To Zipcode"),
    });

    if (
      fromZipcode !== null &&
      toZipcode !== null &&
      fromZipcode !== "" &&
      toZipcode !== ""
    ) {
      fetch(`https://localhost:7228/api/Distance/${fromZipcode}/${toZipcode}`)
        .then((response) => response.json())
        .then((data) => {
          console.log("aasasa=>>", data);
          if (data.status === 200) {
            setAnswer(data.message);
            setMessageClass("alert-success");
          } else {
            setAnswer(data.title);
            setMessageClass("alert-fail");
          }
        })
        .catch((err) => {
          setAnswer(err.title);
          setMessageClass("alert-fail");
        });
    }
  };

  useEffect(() => {
    setFormError({
      ...formError,
      fromZipcode: stringValidate(fromZipcode, "From Zipcode"),
    });
  }, [fromZipcode]);

  useEffect(() => {
    setFormError({
      ...formError,
      toZipcode: stringValidate(toZipcode, "To Zipcode"),
    });
  }, [toZipcode]);

  return (
    <>
      <div className="container">
        <div className="justify-content-center distance-form ">
          <div className="wrap d-md-flex">
            <div className="login-wrap" style={{ width: 500 }}>
              <div className="App-header">
                Find Distance Between Two Zipcodes
              </div>
              <div className="p-rem2">
                <form className="signin-form" onSubmit={submitForm}>
                  <div className="form-group mb-3 is-valid">
                    <label className="label">From Zip</label>
                    <input
                      type="text"
                      name="fromZipcode"
                      className="form-control"
                      placeholder="From zipcode"
                      aria-label="From Zipcode"
                      aria-describedby="basic-addon1"
                      required=""
                      onChange={(e) => setfromZipcode(e.target.value)}
                    />
                  </div>
                  <p className="profile-input-errors">
                    {formError.fromZipcode}
                  </p>
                  <div className="form-group mb-3 is-valid">
                    <label className="label">To Zip</label>
                    <input
                      type="text"
                      name="toZipcode"
                      className="form-control"
                      placeholder="To zipcode"
                      aria-label="To zipcode"
                      aria-describedby="basic-addon1"
                      required=""
                      onChange={(e) => settoZipcode(e.target.value)}
                    />
                  </div>
                  <p className="profile-input-errors">{formError.toZipcode}</p>
                  <div className="form-group">
                    <button
                      type="submit"
                      onClick={submitForm}
                      className="form-control btn btn-primary px-3"
                    >
                      Submit
                    </button>
                  </div>
                  <div
                    className="form-group text-center"
                    style={{ marginTop: 10 }}
                  >
                    <div className="col-xs-12 p-b-20">
                      <label className={messageClass}>{answer}</label>
                    </div>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default FindDistance;
