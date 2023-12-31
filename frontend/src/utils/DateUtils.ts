import StringUtils from "./StringUtils"

class DateUtils {
  public static parse(dateStr: string): Date
  {
    if(StringUtils.isNullOrEmpty(dateStr))
    {
      return null
    }
    return new Date(dateStr)
  }

  public static parseToInputDateFormat(date: Date): string
  {
    const day: number = date.getDate()
    const dayStr: string = StringUtils.fillStartToLength(day.toString(), 2, "0")

    const month: number = date.getMonth() + 1
    const monthStr: string = StringUtils.fillStartToLength(month.toString(), 2, "0")

    const year: number = date.getFullYear()
    const yearStr: string = year.toString()
    return `${yearStr}-${monthStr}-${dayStr}`
  }

  public static parseStringToInputDateFormat(dateStr: string): string
  {
    const date: Date = this.parse(dateStr)
    if(!date)
    {
      return ""
    }
    return this.parseToInputDateFormat(date)
  }
}

export default DateUtils