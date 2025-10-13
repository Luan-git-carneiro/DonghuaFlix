import { AnimeGrid } from "@/featured/anime-grid/anime-grid";
import { FeaturedCarousel } from "@/featured/featured-carousel/featured-carousel";
import { GenreCards } from "@/featured/genre-cards/genre-cards";


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
