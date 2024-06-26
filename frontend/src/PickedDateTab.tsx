import { useState, useEffect } from "react"
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
  const [errorMessage, setErrorMessage] = useState(undefined)
  const [currentIndex, setCurrentIndex] = useState(0)
  const [selectedDate, setSelectedDate] = useState<Dayjs>(dayjs(new Date()))

  useEffect(() => {
    const fetchData = async () => {
      const response = await ajaxService.getDateFloridaMan(selectedDate)
      if (!response.message) {
        setData(response)
      } else {
        setErrorMessage(response.message)
      }
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
            views={["month", "day"]}
          />
        </LocalizationProvider>
      </div>
      <div>
        {data && (
          <NewsItem
            title={data[currentIndex]?.title ?? "No title found"}
            link={data[currentIndex]?.link ?? ""}
            imageLink={data[currentIndex]?.imageLink}
            snippet={data[currentIndex]?.snippet}
            errorMessage={errorMessage}
          />
        )}
      </div>
      <NavigationArrows
        increment={incrementIndex}
        decrement={decrementIndex}
        index={currentIndex}
        maxIndex={data.length - 1}
      />
    </>
  )
}
