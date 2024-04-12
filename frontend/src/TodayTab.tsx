import { useState, useEffect } from "react"
import * as ajaxService from "./services/ajaxService"
import { NewsItem } from "./components/NewsItem"
import { NavigationArrows } from "./components/NavigationArrows"

interface NewsItem {
  title: string
  link: string
  snippet: string | undefined
  imageLink: string | undefined
}

export const TodayTab = () => {
  const [data, setData] = useState<NewsItem[]>([])
  const [errorMessage, setErrorMessage] = useState(undefined)
  const [currentIndex, setCurrentIndex] = useState(0)

  useEffect(() => {
    const fetchData = async () => {
      const response = await ajaxService.getTodayFloridaMan()
      if (!response.message) {
        setData(response)
      } else {
        setErrorMessage(response.message)
      }
    }
    fetchData()
  }, [])

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
      {data && (
        <NewsItem
          title={data[currentIndex]?.title ?? "No title found"}
          link={data[currentIndex]?.link ?? ""}
          imageLink={data[currentIndex]?.imageLink}
          snippet={data[currentIndex]?.snippet}
          errorMessage={errorMessage}
        />
      )}
      <NavigationArrows
        increment={incrementIndex}
        decrement={decrementIndex}
        index={currentIndex}
        maxIndex={data.length - 1}
      />
    </>
  )
}
