function Robot()
{
    var geometry = new THREE.BoxGeometry(0.9, 0.3, 0.9);
    var cubeMaterials = [
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //LEFT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //RIGHT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/robot_top.png"), side: THREE.DoubleSide }), //TOP
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //FRONT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //BACK
    ];
    var material = new THREE.MeshFaceMaterial(cubeMaterials);
    var robot = new THREE.Mesh(geometry, material);
    robot.position.y = 0.15;

    return robot;





}