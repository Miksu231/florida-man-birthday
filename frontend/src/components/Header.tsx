import Tabs from "@mui/material/Tabs"
import Tab from "@mui/material/Tab"

interface HeaderProps {
  tab: number
  setTab: React.Dispatch<React.SetStateAction<number>>
}

export const Header = ({ tab, setTab }: HeaderProps) => {
  const handleChange = (newValue: number) => {
    setTab(newValue)
  }
  return (
    <Tabs
      value={tab}
      onChange={(_, value) => handleChange(value)}
      aria-label="tabs"
    >
      <Tab label="Today" />
      <Tab label="Pick a date" />
    </Tabs>
  )
}
