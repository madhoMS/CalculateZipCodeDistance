

// ------------------------------------   VALIDATING NUMBER INPUT  ------------------------------------**//
export const numberValidate = (input, field) => {
    let error = {};
    if (input === null || input === undefined) {
      error.input = `Please Enter ${field}`;
    } else if (isNaN(input)) error.input = `${field} is invalid`;
    else error.input = null;
  
    return error.input;
  };
  
  // ------------------------------------   VALIDATING STRING INPUT  ------------------------------------**//
  export const stringValidate = (stringinput, field = "") => {
    let error = {};
    if (!stringinput || stringinput === "") {
      error.stringinput = `Please Enter ${field === "" ? "This" : field}`;
    } else if (stringinput.length === 0) {
      error.stringinput = `Please Select ${field}`;
    } else {
      error.stringinput = null;
    }
  
    return error.stringinput;
  };