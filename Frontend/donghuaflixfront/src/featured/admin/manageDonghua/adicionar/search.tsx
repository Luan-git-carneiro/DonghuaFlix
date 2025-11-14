import { Button } from "@/ui/button";
import { Input } from "@/ui/input";
import { Search } from "lucide-react";

interface SearchDonghuaProps {
    isLoading: boolean;
    search: string;
    setSearchQuery: (query: string) => void;
    onSearch: (e: React.FormEvent<HTMLFormElement>) => void;
}

export default function SearchDonghua({ isLoading, search, setSearchQuery, onSearch }: SearchDonghuaProps)
{
    return (
        <form onSubmit={onSearch} className="mb-8">
          <div className="flex gap-2">
            <div className="relative flex-1">
              <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-5 h-5 text-muted-foreground" />
              <Input
                type="text"
                placeholder="Digite o nome do donghua..."
                value={search}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="pl-10 h-12 text-lg"
              />
            </div>
            <Button type="submit" size="lg" disabled={isLoading} className="px-8">
              {isLoading ? "Buscando..." : "Buscar"}
            </Button>
          </div>
        </form>
    )
}