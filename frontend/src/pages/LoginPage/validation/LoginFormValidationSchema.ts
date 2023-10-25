import * as Yup from 'yup';

const LoginFormValidationSchema = Yup.object().shape({
  login: Yup.string()
    .required("Required"),
  password: Yup.string()
    .required("Required")
})

export default LoginFormValidationSchema