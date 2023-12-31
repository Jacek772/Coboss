import GlobalModalClickResultEnum from "../../../components/GlobalModal/types/GlobalModalClickResultEnum"

type UseGlobalModalState = {
  key: string,
  callbacks: ((clickResult: GlobalModalClickResultEnum) => void)[]
}

export default UseGlobalModalState