import { Card, CardContent } from "@/ui/card"

const genres = [
  {
    name: "Ação",
    image: "/chinese-action-anime-scene-with-martial-arts.jpg",
    count: "120+ animes",
  },
  {
    name: "Romance",
    image: "/chinese-romance-anime-scene-with-beautiful-charact.jpg",
    count: "85+ animes",
  },
  {
    name: "Fantasia",
    image: "/chinese-fantasy-anime-scene-with-magical-elements.jpg",
    count: "95+ animes",
  },
  {
    name: "Comédia",
    image: "/chinese-comedy-anime-scene-with-funny-characters.jpg",
    count: "60+ animes",
  },
  {
    name: "Drama",
    image: "/chinese-drama-anime-scene-with-emotional-moments.jpg",
    count: "75+ animes",
  },
  {
    name: "Histórico",
    image: "/chinese-historical-anime-scene-with-ancient-settin.jpg",
    count: "40+ animes",
  },
]

export function GenreCards() {
  return (
    <section className="py-12">
      <div className="container px-4">
        <h2 className="text-3xl font-bold mb-8 text-center">Explore por Gênero</h2>
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-4">
          {genres.map((genre) => (
            <Card key={genre.name} className="group cursor-pointer hover:scale-105 transition-transform duration-300">
              <CardContent className="p-0">
                <div className="relative overflow-hidden rounded-lg">
                  <img
                    src={genre.image || "/placeholder.svg"}
                    alt={genre.name}
                    className="w-full h-32 object-cover group-hover:scale-110 transition-transform duration-300"
                  />
                  <div className="absolute inset-0 bg-gradient-to-t from-black/70 to-transparent" />
                  <div className="absolute bottom-2 left-2 text-white">
                    <h3 className="font-semibold">{genre.name}</h3>
                    <p className="text-xs text-gray-300">{genre.count}</p>
                  </div>
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      </div>
    </section>
  )
}
