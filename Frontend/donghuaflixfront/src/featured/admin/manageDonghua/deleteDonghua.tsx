'use client'

import { Donghua } from "@/Domain/entities/donghua"
import { Button } from "@/ui/button";
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle } from "@/ui/dialog";

interface DeleteDonghuaProps
{
    deletingDonghua: Donghua | null ;
    setDeletingDonghua: (donghua: Donghua | null) => void ;
    confirmDelete: () => void ;
}


export const DeleteDonghua = ( { deletingDonghua , setDeletingDonghua , confirmDelete}: DeleteDonghuaProps ) => 
{
    return (
        <Dialog open={!!deletingDonghua} onOpenChange={() => setDeletingDonghua(null)}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Confirmar Exclusão</DialogTitle>
            <DialogDescription>
              Tem certeza que deseja excluir <strong>{deletingDonghua?.Title}</strong>? Esta ação não pode ser desfeita.
            </DialogDescription>
          </DialogHeader>
          <DialogFooter>
            <Button variant="outline" onClick={() => setDeletingDonghua(null)}>
              Cancelar
            </Button>
            <Button variant="destructive" onClick={confirmDelete}>
              Excluir
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    )
}