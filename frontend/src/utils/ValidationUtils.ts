import { z } from "zod"
import ValidationResult from "./types/ValidationResult"

class ValidationUtils {
  public static validateValue<T>(validationSchema: z.ZodSchema, value: T): ValidationResult {
    if(!validationSchema) {
      return {
        success: true,
        message: ""
      }
    }
    const result: any = validationSchema.safeParse(value)
    return {
      success: result.success,
      message: result.error?.format()?._errors?.join(". ") ?? ""
    }
  }
}

export default ValidationUtils