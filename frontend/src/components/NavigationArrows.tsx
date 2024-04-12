import IconButton from "@mui/material/IconButton"
import backArrow from "../assets/arrow_back.svg"
import forwardArrow from "../assets/arrow_forward.svg"

interface NavigationProps {
  increment: () => void
  decrement: () => void
  index: number
  maxIndex: number
}

export const NavigationArrows = ({
  increment,
  decrement,
  index,
  maxIndex,
}: NavigationProps) => {
  return (
    <>
      <p className="mt-4">
        Page {index + 1} of {maxIndex + 1}{" "}
      </p>
      <div className="flex flex-row rounded border-2 border-slate-300 text-center justify-center mx-auto w-40">
        <IconButton
          onClick={decrement}
          size="small"
          className="rounded border-slate-300 inline-block"
          sx={{ borderRightWidth: 2, borderColor: "black", paddingRight: 1 }}
        >
          <img src={backArrow} alt="back arrow" />
          <p className="text-sm font-semibold text-blue-900">Previous</p>
        </IconButton>
        <IconButton
          onClick={increment}
          size="small"
          className="rounder border-slate-300 inline-block"
          sx={{ borderLeftWidth: 2, borderColor: "black", paddingLeft: 1 }}
        >
          <p className="text-sm font-semibold text-blue-900">Next</p>
          <img src={forwardArrow} alt="forward arrow" />
        </IconButton>
      </div>
    </>
  )
}
