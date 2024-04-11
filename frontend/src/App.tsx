import { useState } from "react"
import { PickedDateTab } from "./PickedDateTab"
import { TodayTab } from "./TodayTab"
import { Header } from "./components/Header"

function App() {
  const [tab, setTab] = useState(0)
  return (
    <div>
      <Header tab={tab} setTab={setTab} />
      {!tab && <TodayTab />}
      {!!tab && <PickedDateTab />}
    </div>
  )
}

export default App
