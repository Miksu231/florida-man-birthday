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
      className="rounded font-sans font-bold mb-4 mt-4 mx-auto text-center justify-center border-2 border-slate-300 bg-gray-100 w-1/2 items-center"
      sx={{ justifyContent: "center", alignItems: "center" }}
    >
      <Tab label="Today" className="justify-center text-center mx-auto w-20" />
      <Tab
        label="Pick a date"
        className="justify-center text-center mx-auto w-20"
      />
    </Tabs>
  )
}
