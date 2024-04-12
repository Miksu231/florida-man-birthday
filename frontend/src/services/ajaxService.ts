import { Dayjs } from "dayjs"

export const getTodayFloridaMan = async () => {
  const response = await fetch(
    new URL(`${import.meta.env.VITE_APIROOT}/floridaman/today`),
    {
      method: "POST",
    }
  )
  return await response.json()
}

export const getDateFloridaMan = async (date: Dayjs) => {
  const response = await fetch(
    new URL(`${import.meta.env.VITE_APIROOT}/floridaman/date`),
    {
      method: "POST",
      headers: new Headers({ "content-type": "application/json" }),
      body: JSON.stringify({
        day: date.date().toString(),
        month: date.format("MMMM"),
      }),
    }
  )
  return await response.json()
}
