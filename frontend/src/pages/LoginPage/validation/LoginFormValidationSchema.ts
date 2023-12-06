import * as Yup from 'yup';

const LoginFormValidationSchema = Yup.object().shape({
  email: Yup.string()
    .required("Required"),
  password: Yup.string()
    .required("Required")
})

export default LoginFormValidationSchema