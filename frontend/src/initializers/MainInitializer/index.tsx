// Initializers

import AuthDataInitializer from "../AuthDataInitializer"
import IMainInitializerProps from "./types/IMainInitializerProps"


const MainInitializer: React.FC<IMainInitializerProps> = ({ children }: IMainInitializerProps) => {
  return <>
  <AuthDataInitializer>{children}</AuthDataInitializer>
  </>
}

export default MainInitializer