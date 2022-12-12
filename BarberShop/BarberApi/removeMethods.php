<?php
   function removePost($connect, $data){
    $deletepost=$connect->prepare("call Post_Delete(?)");
    $deletepost->execute(array($data["id"]));
    $selectPost=$connect->prepare("select * from Post where ID_Post=?");
    $selectPost->execute(array($data["id"]));


    if(count($selectPost->fetchAll())>0){
            $responce=[
                "status"=>false,
                "message"=>"post wasnt delete"
            ];
            echo json_encode($responce);
            die();
    }else{
        $responce=[
            "status"=>true,
            "message"=>"post was deleted"
        ];
        echo json_encode($responce);
        die();
    }
   // $selectUsers=$connect->prepare("Select * from Employe where Email=? or INN=?");

}

function removeVacation($connect, $data){
    $deletepost=$connect->prepare("Delete from Vacation where ID_Vacation=?");
    $deletepost->execute(array($data["id"]));
    $selectPost=$connect->prepare("select * from Vacation where ID_Vacation=?");
    $selectPost->execute(array($data["id"]));


    if(count($selectPost->fetchAll())>0){
            $responce=[
                "status"=>false,
                "message"=>"Vacation wasnt delete"
            ];
            echo json_encode($responce);
            die();
    }else{
        $responce=[
            "status"=>true,
            "message"=>"Vacation was deleted"
        ];
        echo json_encode($responce);
        die();
    }
   // $selectUsers=$connect->prepare("Select * from Employe where Email=? or INN=?");

}


function removeSickLeave($connect, $data){
    $deletepost=$connect->prepare("Delete from SickLeave where ID_SickLeave=?");
    $deletepost->execute(array($data["id"]));
    $selectPost=$connect->prepare("select * from SickLeave where ID_SickLeave=?");
    $selectPost->execute(array($data["id"]));


    if(count($selectPost->fetchAll())>0){
            $responce=[
                "status"=>false,
                "message"=>"SickLeave wasnt delete"
            ];
            echo json_encode($responce);
            die();
    }else{
        $responce=[
            "status"=>true,
            "message"=>"SickLeave was deleted"
        ];
        echo json_encode($responce);
        die();
    }
   // $selectUsers=$connect->prepare("Select * from Employe where Email=? or INN=?");

}






function removeEployerByEmail($connect, $data){
  //  echo $data["email"]; die();
    try{
        $deleteUser =$connect->prepare("Delete from Employe where Email=?");
        $deleteUser ->execute(array($data["email"]));

        $selectUsers=$connect->prepare("Select * from Employe where Email=?");
        $selectUsers->execute(array(strval($data["email"])));
        if(count($selectUsers->fetchAll())>0){
            $responce=[
                "status"=>false,
                "message"=>"user not deleted"
            ];
            echo json_encode($responce);
            die();
        }else{
            $responce=[
                "status"=>false,
                "message"=>"user deleted"
            ];
            echo json_encode($responce);
            die();
        }
    } catch (Exception $e){
        $responce=[
            "status"=>false,
            "message"=>"user not deleted"
        ];
        echo json_encode($responce);
    }

   
}


?>