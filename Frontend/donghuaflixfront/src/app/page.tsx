import { AnimeGrid } from "@/featured/home/anime-grid/anime-grid";
import { FeaturedCarousel } from "@/featured/home/featured-carousel/featured-carousel";
import { GenreCards } from "@/featured/home/genre-cards/genre-cards";


export default function Home() {
  return (
    <div className="min-h-screen bg-background">
      <main>
        <FeaturedCarousel />
        <GenreCards />
        <AnimeGrid />
      </main>
    </div>
  );
}
