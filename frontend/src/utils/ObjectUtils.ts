class ObjectUtils {
  public static getValueByPath<T>(obj: any, path: string): T
  {
    if(!obj)
    {
      return null
    }

    const pathParts: string[] = path.trim().split(".")
    if(pathParts.length === 1)
    {
      return obj[pathParts.at(0)]
    }

    let value: any = obj
    for(const pathPart of pathParts)
    {
      value = value[pathPart]
      if(!value)
      {
        return null
      }
    }
    
    return value as T
  }

  public static setValueByPath<T1, T2>(obj: T1, path: string, value: T2) : T1
  {
    if(!obj)
    {
      return null
    }

    const objCopy: T1 = this.deepCopy(obj)

    const pathParts: string[] = path.trim().split(".")
    if(pathParts.length === 1)
    {
      objCopy[pathParts.at(0)] = value
      return objCopy
    }

    let fieldObj: any = objCopy
    for(const pathPart of pathParts)
    {
      if(pathPart === pathParts.at(-1))
      {
        fieldObj[pathPart] = value
        break
      }

      fieldObj = fieldObj[pathPart]
      if(!fieldObj)
      {
        break
      }
    }
    return objCopy
  }

  public static objectsDifferent(obj1: any, obj2: any): boolean
  {
    return JSON.stringify(obj1) !== JSON.stringify(obj2)
  }

  public static isObjectEmpty(obj: any): boolean
  {
    return JSON.stringify(obj) === "{}"
  }

  public static objectFieldsEmpty(obj: any): boolean
  {
    let empty: boolean = true
    for(const key of Object.keys(obj))
    {
      if(typeof(obj[key]) == "object")
      {
        empty = this.objectFieldsEmpty(obj[key])
        if(!empty)
        {
          break
        }
      }
      else
      {
        const value: string = obj[key]?.toString().trim().replace("?", "")
        if(value)
        {
          empty = false
          break
        }
      }
    }
    return empty
  }
  
  public static deepCopy<T>(obj: T) : T
  {
    return JSON.parse(JSON.stringify(obj)) as T
  }
}

export default ObjectUtils