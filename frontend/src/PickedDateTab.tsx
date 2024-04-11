import { useState, useEffect } from "react"
import CircularProgress from "@mui/material/CircularProgress"
import * as ajaxService from "./services/ajaxService"
import { NewsItem } from "./components/NewsItem"
import { LocalizationProvider } from "@mui/x-date-pickers"
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs"
import { DatePicker } from "@mui/x-date-pickers/DatePicker"
import dayjs, { Dayjs } from "dayjs"
import { NavigationArrows } from "./components/NavigationArrows"

interface NewsItem {
  title: string
  link: string
  snippet: string | undefined
  imageLink: string | undefined
}

export const PickedDateTab = () => {
  const [data, setData] = useState<NewsItem[]>([])
  const [currentIndex, setCurrentIndex] = useState(0)
  const [selectedDate, setSelectedDate] = useState<Dayjs>(dayjs(new Date()))

  useEffect(() => {
    const fetchData = async () => {
      setData(await ajaxService.getDateFloridaMan(selectedDate))
    }
    fetchData()
  }, [selectedDate])

  const incrementIndex = () => {
    let newValue = currentIndex + 1
    if (newValue > data.length - 1) {
      newValue = 0
    }
    setCurrentIndex(newValue)
  }

  const decrementIndex = () => {
    let newValue = currentIndex - 1
    if (newValue < 0) {
      newValue = data.length - 1
    }
    setCurrentIndex(newValue)
  }

  return (
    <>
      <div>
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <DatePicker
            label="Select a date"
            value={selectedDate}
            onChange={(newValue) => setSelectedDate(newValue as Dayjs)}
          />
        </LocalizationProvider>
      </div>
      <div>
        {!data && <CircularProgress />}
        {data && (
          <NewsItem
            title={data[currentIndex]?.title ?? "No title found"}
            link={data[currentIndex]?.link ?? ""}
            imageLink={data[currentIndex]?.imageLink}
            snippet={data[currentIndex]?.snippet}
          />
        )}
      </div>
      <NavigationArrows increment={incrementIndex} decrement={decrementIndex} />
    </>
  )
}
