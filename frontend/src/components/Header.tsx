import Tabs from "@mui/material/Tabs"
import Tab from "@mui/material/Tab"

interface HeaderProps {
  tab: number
  setTab: React.Dispatch<React.SetStateAction<number>>
}

export const Header = ({ tab, setTab }: HeaderProps) => {
  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setTab(newValue)
  }
  return (
    <Tabs value={tab} onChange={handleChange} aria-label="tabs">
      <Tab label="Today" />
      <Tab label="Pick a date" />
    </Tabs>
  )
}
