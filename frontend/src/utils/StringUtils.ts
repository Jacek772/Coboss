class StringUtils {
  public static isNullOrEmpty(text: string): boolean {
    if(!text)
    {
      return true
    }

    text = text.trim()
    return text.length === 0
  }

  public static fillStartToLength(text: string, length: number, fillSign: string): string
  {
    if(text.length >= length)
    {
      return text
    }

    const fillLength: number = length - text.length
    for(let i: number = 0; i < fillLength; i++)
    {
      text = `${fillSign}${text}`
    }
    return text
  }
}

export default StringUtils