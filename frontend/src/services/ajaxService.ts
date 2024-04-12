import { Dayjs } from "dayjs"

export const getTodayFloridaMan = async () => {
  const response = await fetch(
    new URL(`${import.meta.env.APIROOT}/floridaman/today`),
    {
      method: "POST",
    }
  )
  return await response.json()
}

export const getDateFloridaMan = async (date: Dayjs) => {
  const response = await fetch(
    new URL(`${import.meta.env.APIROOT}/floridaman/date`),
    {
      method: "POST",
      body: JSON.stringify({ day: date.date(), month: date.month() }),
    }
  )
  return await response.json()
}
