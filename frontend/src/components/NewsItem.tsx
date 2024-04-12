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
    <div className="flex flex-col border-2 bg-white justify-center content-center w-1/2 h-3/4 mx-auto rounded border-slate-300 mt-4">
      <div className="mt-8 h-12">
        <h2 className="text-slate-800 mb-8">{title}</h2>
      </div>
      {!isLoaded && <CircularProgress sx={{ margin: "auto" }} />}
      {imageLink && (
        <div className="p-8 bg-gray-100 border-2 border-slate-300 mx-auto w-5/6 h-96 justify-center content-center rounded">
          <img
            src={imageLink}
            alt=""
            onLoad={() => setIsLoaded(true)}
            className="mx-auto w-auto h-full max-w-96"
          />
        </div>
      )}
      {snippet && (
        <div className="mt-8 h-16 mx-auto w-5/6">
          <p className="text-sm font-extralight font-serif w-5/6 mx-auto">
            {snippet}
          </p>
        </div>
      )}
      {!snippet && <div className="mt-8 w-5/6 mx-auto h-16"></div>}
      <a
        href={link}
        className="mt-4 text-blue-500 font-extralight mb-4 w-1/3 mx-auto border-2 border-slate-300 rounded"
      >
        Site link
      </a>
    </div>
  )
}
