import IconButton from "@mui/material/IconButton"
import backArrow from "../assets/arrow_back.svg"
import forwardArrow from "../assets/arrow_forward.svg"

interface NavigationProps {
  increment: () => void
  decrement: () => void
}

export const NavigationArrows = ({ increment, decrement }: NavigationProps) => {
  return (
    <div>
      <IconButton onClick={decrement} size="small">
        <img src={backArrow} alt="back arrow" />
        <p>Previous</p>
      </IconButton>
      <IconButton onClick={increment} size="small">
        <p>Next</p>
        <img src={forwardArrow} alt="forward arrow" />
      </IconButton>
    </div>
  )
}
