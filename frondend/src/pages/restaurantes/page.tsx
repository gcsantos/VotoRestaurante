"use client";

import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
} from "@/components/ui/card";
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { api } from "@/services/page";
import { Camera, Check } from "lucide-react";
import { useEffect, useState } from "react";

interface IRestaurante {
  id: number;
  nome: string;
}

export default function Restaurantes() {
  const horaAtual = new Date().getHours();
  const podeVotar = horaAtual >= 8 && horaAtual <= 11;
  const mostrarCampeao = horaAtual >= 12;
  const reiniciarRestaurantes = new Date().getDay();

  if (reiniciarRestaurantes === 1 && podeVotar) {
    alert("Dia de reniciar");
    api.put("/restaurantes/reiniciarRestaurantes").then(() => {});
  }

  const [restaurante, setRestaurante] = useState<IRestaurante[]>();
  const [contatdor, setContador] = useState(0);
  const [nome, setNome] = useState("");
  const [cpf, setCpf] = useState("");
  const [campeao, setCampeao] = useState<IRestaurante[]>();
  const [open, setOpen] = useState(false);

  const handleNomeChange = (e: any) => {
    setNome(e.target.value);
  };

  const handleCpfChange = (e: any) => {
    setCpf(e.target.value);
  };

  useEffect(() => {
    api
      .get("/restaurantes/disponivel", {
        params: { participa: true },
      })
      .then((response) => {
        setRestaurante(response.data);
      });

    api.get("/votos/dia").then((response) => {
      setContador(response.data.length);
    });

    if (!podeVotar && !mostrarCampeao) {
      api.get("/votos/campeao").then((response) => {
        setCampeao(response.data);
        api
          .put(`/restaurantes/${response.data?.[0]?.id}/bloquearRestaurante`)
          .then(() => {});
      });
    }
  }, []);

  function votar() {
    api.post(`/votos/incluir`, dialogModel).then((response) => {
      if (response.data === "OK") {
        alert("Cadastro efetuado com sucesso");
        setOpen(false);
      }
      if (response.data == "cadastrado") {
        alert("CPF já cadastrado");
        setOpen(false);
      }
    });
  }

  const dialogModel = {
    RestauranteId: 0,
    Nome: "",
    Cpf: "",
  };

  return (
    <div className="flex min-h-screen flex-col items-center p-24">
      {podeVotar ? (
        <>
          <h1>Escolha qual seu restaurante favorito?</h1>
          <h2>Vote agora mesmo</h2>
          <h2>Total de Votos {contatdor}</h2>

          <div className="grid gap-4">
            {restaurante?.map((rest) => (
              <div key={rest.id} className="mt-4">
                <Card>
                  <CardHeader>
                    <CardContent className="grid gap-4">
                      <h2>{rest.nome}</h2>
                    </CardContent>
                    <CardFooter>
                      <Dialog open={open} onOpenChange={setOpen}>
                        <DialogTrigger asChild>
                          <Button className="w-full">
                            <Check className="mr-2 h-4 w-4" /> Vote Aqui!
                          </Button>
                        </DialogTrigger>
                        <DialogContent className="sm:max-w-[425px]">
                          <DialogHeader>
                            <DialogTitle>Informações obrigatorias</DialogTitle>
                          </DialogHeader>
                          <div className="grid gap-4 py-4">
                            <div className="grid grid-cols-4 items-center gap-4">
                              <Label htmlFor="name" className="text-right">
                                Nome
                              </Label>
                              <Input
                                id="name"
                                placeholder="Nome"
                                className="col-span-3"
                                onChange={handleNomeChange}
                              />
                            </div>
                            <div className="grid grid-cols-4 items-center gap-4">
                              <Label htmlFor="username" className="text-right">
                                CPF
                              </Label>
                              <Input
                                id="cpf"
                                className="col-span-3"
                                type="number"
                                onChange={handleCpfChange}
                              />
                            </div>
                          </div>
                          <DialogFooter>
                            <Button
                              onClick={() =>
                                votar(
                                  (dialogModel.RestauranteId = rest.id),
                                  (dialogModel.Nome = nome),
                                  (dialogModel.Cpf = cpf)
                                )
                              }
                              type="submit">
                              Confirma voto!
                            </Button>
                          </DialogFooter>
                        </DialogContent>
                      </Dialog>
                    </CardFooter>
                  </CardHeader>
                </Card>
              </div>
            ))}
          </div>
        </>
      ) : (
        <>
          <h2 className="text-3xl font-bold">Voto bloqueado</h2>
          <h1>Restaurante Campeão do dia: {campeao?.[0]?.nome}</h1>
        </>
      )}
    </div>
  );
}
