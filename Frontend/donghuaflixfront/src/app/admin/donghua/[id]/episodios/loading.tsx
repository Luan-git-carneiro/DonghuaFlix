export default function Loading() {
    return (
      <div className="space-y-6">
        <div className="flex items-center gap-4">
          <div className="h-10 w-10 bg-muted animate-pulse rounded-md" />
          <div className="flex-1">
            <div className="h-8 w-64 bg-muted animate-pulse rounded-md" />
            <div className="h-4 w-96 bg-muted animate-pulse rounded-md mt-2" />
          </div>
          <div className="h-10 w-40 bg-muted animate-pulse rounded-md" />
        </div>
  
        <div className="space-y-4">
          {[1, 2, 3].map((i) => (
            <div key={i} className="h-32 bg-muted animate-pulse rounded-lg" />
          ))}
        </div>
      </div>
    )
  }
  