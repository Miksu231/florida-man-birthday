import CircularProgress from "@mui/material/CircularProgress"
import { useState } from "react"

interface NewsItemProps {
  title: string
  link: string
  snippet: string | undefined
  imageLink: string | undefined
}

export const NewsItem = ({
  title,
  link,
  snippet,
  imageLink,
}: NewsItemProps) => {
  const [isLoaded, setIsLoaded] = useState(false)
  return (
    <div>
      <h2>{title}</h2>
      {snippet && <p>{snippet}</p>}
      {!isLoaded && <CircularProgress />}
      {imageLink && (
        <img src={imageLink} alt="" onLoad={() => setIsLoaded(true)} />
      )}
      <a href={link}>Link</a>
    </div>
  )
}
