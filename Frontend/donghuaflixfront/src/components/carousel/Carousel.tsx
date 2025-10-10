

import { useState, useEffect } from "react"
import { Button } from "@/ui/button"
import { Play, Info, ChevronLeft, ChevronRight } from "lucide-react"
import { Donghua } from "@/Domain/entities/donghua"
import Link from "next/link";



interface CarouselProps {
    Donghuas: Donghua[];
  }

export function Carousel( {Donghuas}:  CarouselProps) {
  const [currentSlide, setCurrentSlide] = useState(0)

  useEffect(() => {
    const timer = setInterval(() => {
      setCurrentSlide((prev) => (prev + 1) % Donghuas.length)
    }, 5000)
    return () => clearInterval(timer)
  }, [])

  const nextSlide = () => {
    setCurrentSlide((prev) => (prev + 1) % Donghuas.length)
  }

  const prevSlide = () => {
    setCurrentSlide((prev) => (prev - 1 + Donghuas.length) % Donghuas.length)
  }

  const currentAnime = Donghuas[currentSlide]

  return (
    <div className="relative h-[70vh] overflow-hidden rounded-lg">
      <div
        className="absolute inset-0 bg-cover bg-center transition-all duration-1000"
        style={{ backgroundImage: `url(${currentAnime.Image})` }}
      >
        <div className="absolute inset-0 bg-gradient-to-r from-black/70 via-black/50 to-transparent" />
      </div>

      <div className="relative z-10 flex h-full items-center">
        <div className="container px-4">
          <div className="max-w-2xl space-y-6 text-white">
            <div className="space-y-2">
              <p className="text-sm font-medium text-accent">{currentAnime.Genre}</p>
              <h1 className="text-4xl md:text-6xl font-bold text-balance">{currentAnime.Title}</h1>
            </div>

            <p className="text-lg text-gray-200 text-pretty max-w-xl">{currentAnime.Sinopse}</p>

            <div className="flex items-center space-x-4">
              <Button size="lg" className="bg-primary hover:bg-primary/90">
                <Play className="w-5 h-5 mr-2" />
                Assistir Agora
              </Button>
              <Link href={`/donghua/${currentAnime.DonghuaId}`}>
                <Button
                size="lg"
                variant="outline"
                className="border-white text-white hover:bg-white hover:text-black bg-transparent"
                >
                  <Info className="w-5 h-5 mr-2" />
                  Mais Info
                </Button>
              </Link>
              
            </div>
          </div>
        </div>
      </div>

      {/* Navigation Arrows */}
      <button
        onClick={prevSlide}
        className="absolute left-4 top-1/2 -translate-y-1/2 z-20 p-2 rounded-full bg-black/50 text-white hover:bg-black/70 transition-colors"
      >
        <ChevronLeft className="w-6 h-6" />
      </button>
      <button
        onClick={nextSlide}
        className="absolute right-4 top-1/2 -translate-y-1/2 z-20 p-2 rounded-full bg-black/50 text-white hover:bg-black/70 transition-colors"
      >
        <ChevronRight className="w-6 h-6" />
      </button>

      {/* Dots Indicator */}
      <div className="absolute bottom-6 left-1/2 -translate-x-1/2 z-20 flex space-x-2">
        {Donghuas.map((_, index) => (
          <button
            key={index}
            onClick={() => setCurrentSlide(index)}
            className={`w-3 h-3 rounded-full transition-colors ${
              index === currentSlide ? "bg-primary" : "bg-white/50"
            }`}
          />
        ))}
      </div>
    </div>
  )
}
