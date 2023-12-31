import IFiltersBarItemData from "../../../components/FiltersBar/types/IFiltersBarItemData";
import FiltersBarItemType from "../../../components/FiltersBarItem/types/enums/FiltersBarItemType";

const filterBarItems: IFiltersBarItemData[] = [
  { name:"period", type: FiltersBarItemType.DatePeriod, label: "Period"  }
]

export default filterBarItems