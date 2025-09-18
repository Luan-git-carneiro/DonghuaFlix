import { AnimeGrid } from "@/featured/anime-grid/anime-grid";
import { FeaturedCarousel } from "@/featured/featured-carousel/featured-carousel";
import { GenreCards } from "@/featured/genre-cards/genre-cards";
import { Footer } from "@/layout/Footer/footer";
import { Header } from "@/layout/Header/header";
import { Button } from "@/ui/button"

export default function Home() {
  return (
    <div className="min-h-screen bg-background">
      <Header />
      <main>
        <FeaturedCarousel />
        <GenreCards />
        <AnimeGrid />
      </main>
      <Footer />
    </div>
  );
}
