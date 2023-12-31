import * as z from 'zod';
import ILoginFormValues from '../types/ILoginFormValues';

const LoginFormValidationSchema = z.object({
  email: z.string()
    .min(1, { message: 'Required' }),
  password: z.string()
    .min(1, { message: 'Required' }),
})

export default LoginFormValidationSchema