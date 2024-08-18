using UnityEngine;

public class NpcFastOrbitX : NpcWalkState
{
    float minSpeed = 200f;
    float maxSpeed = 450f;
    float timer = 0f; // Temporizador

    public override void EnterState(NpcStateManager npcStateManager)
    {
        // Reinicia el temporizador al entrar en el estado
        timer = 0f;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        // Realiza la rotación alrededor del target
        float speed = Random.Range(minSpeed, maxSpeed);
        npcStateManager.transform.RotateAround(npcStateManager.target.gameObject.transform.position, Vector3.up, speed * Time.deltaTime);

        // Incrementa el temporizador con el tiempo transcurrido
        timer += Time.deltaTime;

        // Verifica si han pasado 1.5 segundos
        if (timer >= 0.65f)
        {
            // Opcional: Reinicia el temporizador si quieres que el mensaje se imprima cada 1.5 segundos.
            npcStateManager.SwitchWalkState(npcStateManager.walkingXState);
            npcStateManager.SwitchShootState(npcStateManager.burstShoot);
        }
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Puedes agregar cualquier lógica necesaria al finalizar el estado
    }
}
