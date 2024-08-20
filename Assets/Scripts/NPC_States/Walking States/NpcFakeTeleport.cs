using UnityEngine;

public class NpcFakeTeleport : NpcWalkState
{
    float speed = 2500f;
    float timer = 0f;
    float min = 0.1f;
    float max = 0.25f;

    public override void EnterState(NpcStateManager npcStateManager)
    {
        // Reinicia el temporizador al entrar en el estado
        timer = 0f;
    }

    public override void UpdateState(NpcStateManager npcStateManager)
    {
        npcStateManager.transform.RotateAround(npcStateManager.target.gameObject.transform.position, npcStateManager.speedDir * Vector3.up, speed * Time.deltaTime);

        // Incrementa el temporizador con el tiempo transcurrido
        timer += Time.deltaTime;

        // Verifica si han pasado 1.5 segundos
        if (timer >= Random.Range(min, max))
        {
            // Opcional: Reinicia el temporizador si quieres que el mensaje se imprima cada 1.5 segundos.
            npcStateManager.SwitchWalkState(npcStateManager.walkingXState);
            npcStateManager.SwitchShootState(npcStateManager.simpleShoot);
        }
    }

    public override void EndState(NpcStateManager npcStateManager)
    {
        // Puedes agregar cualquier lógica necesaria al finalizar el estado
    }
}
