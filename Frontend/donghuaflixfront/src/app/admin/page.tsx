"use client"

import { Card, CardContent, CardHeader, CardTitle } from "@/ui/card"
import { Film, Users, Eye, TrendingUp, Star, Clock } from "lucide-react"

export default function AdminDashboard() {
  // Dados simulados - em produção viriam de uma API
  const stats = [
    {
      title: "Total de Donghua",
      value: "156",
      change: "+12 este mês",
      icon: Film,
      color: "text-blue-500",
      bgColor: "bg-blue-500/10",
    },
    {
      title: "Usuários Ativos",
      value: "2,847",
      change: "+18% este mês",
      icon: Users,
      color: "text-green-500",
      bgColor: "bg-green-500/10",
    },
    {
      title: "Visualizações",
      value: "45,231",
      change: "+23% esta semana",
      icon: Eye,
      color: "text-purple-500",
      bgColor: "bg-purple-500/10",
    },
    {
      title: "Taxa de Crescimento",
      value: "12.5%",
      change: "+2.3% este mês",
      icon: TrendingUp,
      color: "text-orange-500",
      bgColor: "bg-orange-500/10",
    },
  ]

  const recentActivity = [
    {
      user: "João Silva",
      action: "Assistiu",
      anime: "Mo Dao Zu Shi - Ep 5",
      time: "2 minutos atrás",
    },
    {
      user: "Maria Santos",
      action: "Adicionou à lista",
      anime: "The King's Avatar",
      time: "15 minutos atrás",
    },
    {
      user: "Pedro Costa",
      action: "Comentou em",
      anime: "Heaven Official's Blessing",
      time: "1 hora atrás",
    },
    {
      user: "Ana Lima",
      action: "Avaliou",
      anime: "Link Click - 5 estrelas",
      time: "2 horas atrás",
    },
    {
      user: "Carlos Mendes",
      action: "Assistiu",
      anime: "Scissor Seven - Ep 3",
      time: "3 horas atrás",
    },
  ]

  const topDonghua = [
    { title: "Mo Dao Zu Shi", views: "12,543", rating: 4.9 },
    { title: "The King's Avatar", views: "10,234", rating: 4.8 },
    { title: "Heaven Official's Blessing", views: "9,876", rating: 4.9 },
    { title: "Link Click", views: "8,432", rating: 4.7 },
    { title: "Scissor Seven", views: "7,654", rating: 4.6 },
  ]

  return (
    <div className="space-y-8">
      <div>
        <h1 className="text-3xl font-bold">Dashboard</h1>
        <p className="text-muted-foreground mt-2">Visão geral do sistema de streaming</p>
      </div>

      {/* Stats Grid */}
      <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-4">
        {stats.map((stat) => {
          const Icon = stat.icon
          return (
            <Card key={stat.title}>
              <CardHeader className="flex flex-row items-center justify-between pb-2">
                <CardTitle className="text-sm font-medium text-muted-foreground">{stat.title}</CardTitle>
                <div className={`p-2 rounded-lg ${stat.bgColor}`}>
                  <Icon className={`h-5 w-5 ${stat.color}`} />
                </div>
              </CardHeader>
              <CardContent>
                <div className="text-3xl font-bold">{stat.value}</div>
                <p className="text-xs text-muted-foreground mt-1">{stat.change}</p>
              </CardContent>
            </Card>
          )
        })}
      </div>

      <div className="grid gap-6 lg:grid-cols-2">
        {/* Top Donghua */}
        <Card>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Star className="h-5 w-5 text-yellow-500" />
              Top 5 Donghua Mais Assistidos
            </CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {topDonghua.map((anime, index) => (
                <div key={index} className="flex items-center justify-between">
                  <div className="flex items-center gap-3">
                    <div className="flex h-8 w-8 items-center justify-center rounded-full bg-primary/10 text-sm font-bold text-primary">
                      {index + 1}
                    </div>
                    <div>
                      <p className="font-medium">{anime.title}</p>
                      <div className="flex items-center gap-3 text-sm text-muted-foreground">
                        <span className="flex items-center gap-1">
                          <Eye className="h-3 w-3" />
                          {anime.views}
                        </span>
                        <span className="flex items-center gap-1">
                          <Star className="h-3 w-3 fill-yellow-500 text-yellow-500" />
                          {anime.rating}
                        </span>
                      </div>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>

        {/* Recent Activity */}
        <Card>
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Clock className="h-5 w-5 text-blue-500" />
              Atividade Recente
            </CardTitle>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {recentActivity.map((activity, index) => (
                <div key={index} className="flex items-start justify-between border-b pb-4 last:border-0 last:pb-0">
                  <div>
                    <p className="font-medium">
                      {activity.user} <span className="text-muted-foreground font-normal">{activity.action}</span>
                    </p>
                    <p className="text-sm text-muted-foreground">{activity.anime}</p>
                  </div>
                  <span className="text-xs text-muted-foreground whitespace-nowrap">{activity.time}</span>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>
      </div>
    </div>
  )
}
