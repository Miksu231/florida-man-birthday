import { useState } from "react"
import { PickedDateTab } from "./PickedDateTab"
import { TodayTab } from "./TodayTab"
import { Header } from "./components/Header"

function App() {
  const [tab, setTab] = useState(0)
  return (
    <div className="bg-gray-100 justify-center text-center font-bold font-serif">
      <h1 className="text-3xl">Find your Florida Man</h1>
      <Header tab={tab} setTab={setTab} />
      {!tab && <TodayTab />}
      {!!tab && <PickedDateTab />}
    </div>
  )
}

export default App
